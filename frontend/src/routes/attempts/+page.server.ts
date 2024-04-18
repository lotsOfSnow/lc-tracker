import { apiClient, getApiOperation } from '$lib/api';
import { error } from '@sveltejs/kit';
import { z } from 'zod';
import { parseRequestFormData } from '$lib/utils/zodUtils';

export const load = async ({ fetch }) => {
  const operation = getApiOperation('/api/attempts', 'get', 200);

  const result = await apiClient.GET(operation.path, {
    fetch,
  });

  return result.data ?? error(400);
};

export const actions = {
  deleteAttempt: async (event) => {
    const { id } = await parseRequestFormData(event.request, deleteFormSchema);

    const operation = getApiOperation('/api/attempts/{id}', 'delete', 204);

    await apiClient.DELETE(operation.path, {
      params: {
        path: {
          id,
        },
      },
    });
  },
};

const deleteFormSchema = z.object({
  id: z.string().uuid(),
});
