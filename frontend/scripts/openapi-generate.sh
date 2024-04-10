PUBLIC_API_URL=$(dotenv -p PUBLIC_API_URL)

if [ -z "$PUBLIC_API_URL" ]; then
 echo "Please specify PUBLIC_API_URL in the .env file."
 exit 1
fi

echo "🔑 Disabling NODE_TLS_REJECT_UNAUTHORIZED to allow introspecting HTTPS API."
export NODE_TLS_REJECT_UNAUTHORIZED=0

openapi-typescript "$PUBLIC_API_URL" -o src/lib/types/openapi.d.ts
