import { apiClient, getApiOperation } from '$lib/api/apiClient';
import { error } from '@sveltejs/kit';

const operation = getApiOperation('/api/problems/{id}', 'get', 200);

export const load = async ({ params, fetch }) => {
  const id = params.id;

  const result = await apiClient.GET(operation.path, {
    fetch,
    params: {
      path: { id: id },
    },
  });

  return result.data ?? error(400);
};
