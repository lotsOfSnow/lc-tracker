ENV_NAME=VITE_PUBLIC_API_URL;
API_URL=$(dotenv -p $ENV_NAME)

if [ -z "$API_URL" ]; then
 echo "Please specify $(ENV_NAME) in the .env file."
 exit 1
fi

echo "🔑 Disabling NODE_TLS_REJECT_UNAUTHORIZED to allow introspecting HTTPS API."
export NODE_TLS_REJECT_UNAUTHORIZED=0

openapi-typescript "$API_URL" -o src/lib/api/openapi.d.ts
