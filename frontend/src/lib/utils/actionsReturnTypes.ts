import { z } from 'zod';
import { type ActionFailure, fail } from '@sveltejs/kit';

export const failValidation = <T>(
  error: z.SafeParseError<T>,
): ActionFailure<ValidationFailData> => {
  return fail(400, { validationError: error.error.flatten() });
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

  const detailKey = 'detail';
  const detail =
    detailKey in error ? (error[detailKey] as string[]) : undefined;

  return fail(400, { serverError: { title, detail } });
};

export type ValidationFailData = {
  validationError: ValidationError;
};

export type ServerFailData = {
  serverError: ServerError;
};

export type ServerError = {
  title: string;
  detail?: string[];
};

export type ValidationError = z.typeToFlattenedError<any>;
