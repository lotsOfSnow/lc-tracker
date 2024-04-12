import type { components } from '$lib/types/openapi';

export const load = async ({
  fetch,
}): Promise<{ problems: components['schemas']['Problem'][] }> => {
  const url = '/api/problems';
  const res = await fetch(url);
  const problems = await res.json();

  return { problems };
};
