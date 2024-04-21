import { z } from 'zod';
import { difficulties, type Difficulty } from './difficulty';

export const baseSchema = z.object({
  problemId: z.string().uuid(),
  minutesSpent: z.coerce.number().min(1),
  date: z.string(),
  difficulty: z
    .custom<Difficulty>((val) => {
      return typeof val === 'string' || typeof val === 'number'
        ? val in difficulties
        : false;
    })
    .pipe(z.coerce.number())
    .transform((val) => val as Difficulty),
  hasUsedHelp: z.coerce.boolean().default(false),
  hasSolved: z.coerce.boolean().default(false),
  isRecap: z.coerce.boolean().default(false),
  note: z.string(),
});
