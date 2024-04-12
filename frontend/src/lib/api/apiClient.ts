import type { ApiResult } from '$lib/api/apiResult';
import type { ApiProblemDetails } from '$lib/api/apiProblemDetails';

export const apiPostJson = async (
  f: typeof fetch,
  url: string,
  data?: unknown,
): Promise<ApiResult> => {
  const body = data ? JSON.stringify(data) : undefined;
  const options = getOptions('POST', body);

  const result = await f(url, options);

  if (result.ok) {
    return { value: result };
  }

  const problemDetails = (await result.json()) as ApiProblemDetails;

  return { error: problemDetails };
};

const getOptions = (method: string, body?: string): RequestInit => {
  return {
    headers: {
      'Content-Type': 'application/json',
    },
    cache: 'no-cache',
    method,
    body,
  };
};
