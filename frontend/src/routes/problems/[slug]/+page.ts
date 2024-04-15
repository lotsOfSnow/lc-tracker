import { apiClient } from '$lib/api/apiClient';

export const load = async ({ params, fetch }) => {
  const id = params.slug;

  const result = await apiClient.GET('/api/problems/{id}', {
    fetch,
    params: {
      path: { id: id },
    },
  });
};
