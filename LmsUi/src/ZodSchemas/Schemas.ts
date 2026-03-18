import { z } from "zod";

export const employeeSchema = z.object({
  name: z.string().min(2, "Name is required"),
  email: z.string().email("Invalid email address"),
  designation: z.string()
//     errorMap: () => ({ message: "Please select a designation" }),
//   }),
});



export type EmployeeFormValues = z.infer<typeof employeeSchema>;
