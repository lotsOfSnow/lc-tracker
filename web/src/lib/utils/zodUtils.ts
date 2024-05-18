import { z, type ZodTypeAny } from 'zod';

export const safeParseRequestFormData = async <T>(
  request: Request,
  schema: z.ZodSchema<T> | z.ZodIntersection<ZodTypeAny, ZodTypeAny>,
): Promise<z.SafeParseReturnType<T, T>> => {
  const formData = await request.formData();
  const entries = Object.fromEntries(formData);
  return schema.safeParse(entries);
};

export const parseRequestFormData = async <T>(
  request: Request,
  schema: z.ZodSchema<T>,
): Promise<T> => {
  const formData = await request.formData();
  const entries = Object.fromEntries(formData);

  const result = schema.safeParse(entries);
  if (result.success) {
    return result.data;
  }

  throw new Error('Failed to parse');
};
