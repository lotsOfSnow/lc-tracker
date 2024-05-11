import { z } from 'zod';

const methodSchema = z.object({
  name: z.string().min(1),
  contents: z.string().min(1),
});

export const problemSchema = z.object({
  slug: z.string().min(2),
  methods: z.string().transform((json) => {
    const parsed = JSON.parse(json);
    return z.array(methodSchema).parse(parsed);
  }),
});
