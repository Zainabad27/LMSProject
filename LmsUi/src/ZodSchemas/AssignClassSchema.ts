import z from "zod";

export const assignClassSchema = z.object({
  teacherId: z.string().min(1, "Select a teacher"),
  classId: z.string().min(1, "Select a class"),
});


export type AssignClassFormValues = z.infer<typeof assignClassSchema>;