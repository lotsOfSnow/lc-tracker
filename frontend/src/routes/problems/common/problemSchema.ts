import { z } from 'zod';

const methodSchema = z.object({
  name: z.string().min(1),
  contents: z.string().min(1),
});

export const problemSchema = z.object({
  note: z.string().optional(),
  methods: z.string().transform((json) => {
    const parsed = JSON.parse(json);
    return z.array(methodSchema).parse(parsed);
  }),
});
