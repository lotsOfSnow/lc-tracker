import { error } from '@sveltejs/kit';
import { apiClient, getApiOperation } from '$lib/api';

const operation = getApiOperation('/api/problems', 'get', 200);

export const load = async ({ fetch }) => {
  const problems = await apiClient.GET(operation.path, { fetch });

  if (problems.data) {
    return problems.data;
  }

  return error(400);
};
