import { apiClient, getApiOperation } from '$lib/api';
import { error } from '@sveltejs/kit';

export const load = async ({ fetch }) => {
  const operation = getApiOperation('/api/attempts', 'get', 200);

  const result = await apiClient.GET(operation.path, {
    fetch,
  });

  return result.data ?? error(400);
};
