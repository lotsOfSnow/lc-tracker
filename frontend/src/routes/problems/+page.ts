import type { components } from '$lib/api';

export const load = async ({
  fetch,
}): Promise<{ problems: components['schemas']['Problem'][] }> => {
  const url = '/api/problems';
  const res = await fetch(url);
  const problems = await res.json();

  return { problems };
};
