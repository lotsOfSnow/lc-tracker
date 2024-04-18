import { z } from 'zod';

export const safeParseRequestFormData = async <T>(
  request: Request,
  schema: z.ZodSchema<T>,
): Promise<z.SafeParseReturnType<T, T>> => {
  const formData = await request.formData();
  const entries = Object.fromEntries(formData);

  return schema.safeParse(entries);
};
