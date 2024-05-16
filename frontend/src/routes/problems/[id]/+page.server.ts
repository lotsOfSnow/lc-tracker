import { type Actions } from '@sveltejs/kit';
import { z } from 'zod';
import { apiClient, getApiOperation } from '$lib/api';
import { safeParseRequestFormData } from '$lib/utils/zodUtils';
import { problemSchema } from '../common/problemSchema';
import { failServer, failValidation } from '$lib/utils/actionsReturnTypes';

export const actions = {
  default: async (event) => {
    const parsingResult = await safeParseRequestFormData(event.request, schema);
    if (!parsingResult.success) {
      return failValidation(parsingResult);
    }

    const operation = getApiOperation('/api/problems/{id}', 'put', 204);

    const request = {
      ...parsingResult.data,
    };

    const result = await apiClient.PUT(operation.path, {
      body: request,
      fetch: event.fetch,
      params: {
        path: {
          id: parsingResult.data.id,
        },
      },
    });

    if (result.data) {
      return { success: true };
    }

    return failServer(result.error);
  },
} satisfies Actions;

const schema = z.intersection(
  problemSchema,
  z.object({
    id: z.string().uuid(),
  }),
);
