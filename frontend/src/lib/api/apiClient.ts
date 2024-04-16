import createClient from 'openapi-fetch';
import type { paths } from '$lib/api/openapi';

const apiUrl = import.meta.env.VITE_PUBLIC_API_URL;

export const apiClient = createClient<paths>({ baseUrl: apiUrl });

type EndpointWithResponses<
  Path extends keyof paths,
  Method extends keyof paths[Path],
> = paths[Path][Method] extends { responses: infer R } ? R : never;

type OperationSelector<
  Path extends keyof paths,
  Method extends keyof paths[Path],
  StatusCode extends keyof EndpointWithResponses<Path, Method>,
> = EndpointWithResponses<Path, Method>[StatusCode] extends {
  content: Record<'application/json', infer ContentType>;
}
  ? ContentType
  : never;

export const getApiOperation = <
  Path extends keyof paths,
  Method extends keyof paths[Path],
  StatusCode extends keyof EndpointWithResponses<Path, Method>,
>(
  path: Path,
  method: Method,
  statusCode: StatusCode,
): {
  path: Path;
  method: Method;
  statusCode: StatusCode;
  responseType: OperationSelector<Path, Method, StatusCode>;
} => {
  return {
    path,
    method,
    statusCode,
    responseType: {} as OperationSelector<Path, Method, StatusCode>,
  };
};
