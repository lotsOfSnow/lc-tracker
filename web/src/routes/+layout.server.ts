import type { LayoutServerLoad } from './$types';
import { apiClient, getApiOperation } from '$lib/api';
import { failServer } from '$lib/utils/actionsReturnTypes';

export const load: LayoutServerLoad = async () => {
  const operation = getApiOperation('/api/api-stats', 'get', 200);
  const result = await apiClient.GET(operation.path);

  if (!result.data) {
    return failServer(result.error);
  }

  return {
    env: result.data.environment,
  };
};
