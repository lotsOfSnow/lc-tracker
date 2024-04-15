import { apiClient } from '$lib/api/apiClient';
import { error } from '@sveltejs/kit';
import type { components } from '$lib/api';

export const load = async ({
  fetch,
}): Promise<components['schemas']['Problem'][]> => {
  const problems = await apiClient.GET('/api/problems', { fetch });

  if (problems.data) {
    return problems.data;
  }

  error(400);
};
