import type { ApiResult } from '$lib/api/apiResult';
import type { ApiProblemDetails } from '$lib/api/apiProblemDetails';
import type { paths } from '$lib/api/openapi';

export type ApiUrl = keyof paths;

export const apiPostJson = async (
  f: typeof fetch,
  url: ApiUrl,
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

export const apiGetJson = async (
  f: typeof fetch,
  url: ApiUrl,
): Promise<any | undefined> => {
  const options = getOptions('GET');

  const result = await f(url, options);

  if (result.ok) {
    try {
      return await result.json();
    } catch (error) {
      throw new Error(
        `Failed to serialize successful response from ${url} to JSON`,
      );
    }
  }

  return undefined;
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
