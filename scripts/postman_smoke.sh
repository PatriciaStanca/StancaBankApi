#!/usr/bin/env bash
set -euo pipefail

LOG_FILE="/tmp/stanca_postman_smoke.log"
ACCOUNT_TYPE_ID="${ACCOUNT_TYPE_ID:-9751}"
ADMIN_USERNAME="${ADMIN_USERNAME:-admin}"
ADMIN_PASSWORD="${ADMIN_PASSWORD:-ChangeMe_Admin123!}"

choose_port() {
  for port in $(seq 5099 5120); do
    if ! nc -z 127.0.0.1 "$port" >/dev/null 2>&1; then
      echo "$port"
      return 0
    fi
  done

  echo "No free port found in range 5099-5120" >&2
  return 1
}

PORT="$(choose_port)"
BASE_URL="http://127.0.0.1:${PORT}"

cleanup() {
  if [[ -n "${API_PID:-}" ]]; then
    kill "$API_PID" 2>/dev/null || true
    wait "$API_PID" 2>/dev/null || true
  fi
}
trap cleanup EXIT

ASPNETCORE_ENVIRONMENT=Development dotnet ./bin/Debug/net9.0/StancaBankApi.dll --urls "$BASE_URL" >"$LOG_FILE" 2>&1 &
API_PID=$!

up=0
for _ in $(seq 1 40); do
  code=$(curl -s -o /tmp/stanca_startup_probe.json -w "%{http_code}" -X POST "$BASE_URL/api/admin/login" \
    -H "Content-Type: application/json" \
    -d "{\"username\":\"$ADMIN_USERNAME\",\"password\":\"$ADMIN_PASSWORD\"}" || true)
  if [[ "$code" == "200" || "$code" == "401" || "$code" == "400" ]]; then
    up=1
    break
  fi
  sleep 1
done

if [[ "$up" != "1" ]]; then
  echo "STARTUP:FAIL"
  echo "---- LOG ----"
  tail -n 120 "$LOG_FILE" || true
  exit 1
fi
echo "STARTUP:OK"
echo "BASE_URL:$BASE_URL"

admin_login=$(curl -s -X POST "$BASE_URL/api/admin/login" \
  -H "Content-Type: application/json" \
  -d "{\"username\":\"$ADMIN_USERNAME\",\"password\":\"$ADMIN_PASSWORD\"}")
admin_token=$(echo "$admin_login" | sed -n 's/.*"token":"\([^"]*\)".*/\1/p')

if [[ -z "$admin_token" ]]; then
  echo "ADMIN_LOGIN:FAIL"
  echo "$admin_login"
  exit 1
fi
echo "ADMIN_LOGIN:OK"

stamp=$(date +%s)
cust_user="cust_${stamp}"
cust_pass="Kund123!"

create_customer=$(curl -s -w "\nHTTP_STATUS:%{http_code}" -X POST "$BASE_URL/api/admin/customers" \
  -H "Authorization: Bearer $admin_token" \
  -H "Content-Type: application/json" \
  -d "{\"givenName\":\"Postman\",\"surname\":\"Smoke\",\"accountId\":$ACCOUNT_TYPE_ID,\"password\":\"$cust_pass\",\"emailAddress\":\"$cust_user@example.com\"}")
echo "CREATE_CUSTOMER:$create_customer"

cust_login=$(curl -s -X POST "$BASE_URL/api/customer/login" \
  -H "Content-Type: application/json" \
  -d "{\"emailAddress\":\"$cust_user@example.com\",\"password\":\"$cust_pass\"}")
cust_token=$(echo "$cust_login" | sed -n 's/.*"token":"\([^"]*\)".*/\1/p')

if [[ -z "$cust_token" ]]; then
  echo "CUSTOMER_LOGIN:FAIL"
  echo "$cust_login"
  exit 1
fi
echo "CUSTOMER_LOGIN:OK"

accounts_1=$(curl -s -w "\nHTTP_STATUS:%{http_code}" "$BASE_URL/api/account" \
  -H "Authorization: Bearer $cust_token")
echo "GET_ACCOUNTS_1:$accounts_1"

create_second=$(curl -s -w "\nHTTP_STATUS:%{http_code}" -X POST "$BASE_URL/api/account" \
  -H "Authorization: Bearer $cust_token" \
  -H "Content-Type: application/json" \
  -d '{"accountTypeId":1}')
echo "CREATE_SECOND_ACCOUNT:$create_second"

accounts_json=$(curl -s "$BASE_URL/api/account" -H "Authorization: Bearer $cust_token")
ids=$(echo "$accounts_json" | rg -o '"id":[0-9]+' | sed 's/"id"://' || true)
from_id=$(echo "$ids" | sed -n '1p')
to_id=$(echo "$ids" | sed -n '2p')

if [[ -n "${from_id:-}" && -n "${to_id:-}" ]]; then
  transfer=$(curl -s -w "\nHTTP_STATUS:%{http_code}" -X POST "$BASE_URL/api/transaction/transfer" \
    -H "Authorization: Bearer $cust_token" \
    -H "Content-Type: application/json" \
    -d "{\"fromAccountId\":$from_id,\"toAccountNumber\":\"$to_id\",\"amount\":1.00,\"description\":\"smoke transfer\"}")
  echo "TRANSFER_OWN:$transfer"

  tx=$(curl -s -w "\nHTTP_STATUS:%{http_code}" "$BASE_URL/api/transaction/account/$from_id" \
    -H "Authorization: Bearer $cust_token")
  echo "TX_FOR_FROM_ACCOUNT:$tx"
else
  echo "TRANSFER_OWN:SKIPPED (not enough accounts)"
fi

role_a=$(curl -s -o /tmp/role_a.txt -w "%{http_code}" -X POST "$BASE_URL/api/admin/customers" \
  -H "Authorization: Bearer $cust_token" \
  -H "Content-Type: application/json" \
  -d "{\"givenName\":\"Bad\",\"surname\":\"User\",\"accountId\":$ACCOUNT_TYPE_ID,\"password\":\"Kund123!\",\"emailAddress\":\"bad_$(date +%s)@example.com\"}")
echo "ROLE_TEST_CUSTOMER_TO_ADMIN:$role_a"

role_b=$(curl -s -o /tmp/role_b.txt -w "%{http_code}" "$BASE_URL/api/account" \
  -H "Authorization: Bearer $admin_token")
echo "ROLE_TEST_ADMIN_TO_CUSTOMER:$role_b"

echo "SMOKE_TEST:DONE"
