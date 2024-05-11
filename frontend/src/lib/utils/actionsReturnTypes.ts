import { z } from 'zod';
import { type ActionFailure, fail } from '@sveltejs/kit';

export const failValidation = <T>(
  error: z.SafeParseError<T>,
): ActionFailure<ValidationFailData> => {
  return fail(400, { messages: error.error.flatten() });
};

export const failServer = (
  error: unknown,
): ActionFailure<ServerFailData | undefined> => {
  const isObject =
    typeof error === 'object' && !Array.isArray(error) && error !== null;
  const titleKey = 'title';
  if (!isObject || !(titleKey in error)) {
    return fail(500);
  }

  const title = error[titleKey] as string;

  const detailsKey = 'details';
  const details =
    detailsKey in error ? (error[detailsKey] as string[]) : undefined;

  return fail(400, { title, details });
};

type ValidationFailData = {
  messages: z.typeToFlattenedError<unknown>;
};

type ServerFailData = {
  title: string;
  details?: string[];
};
