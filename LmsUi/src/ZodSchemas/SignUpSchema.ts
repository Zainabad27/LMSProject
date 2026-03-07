import z from "zod";
const MAX_FILE_SIZE = 5 * 1024 * 1024; 
const ACCEPTED_IMAGE_TYPES = ["image/jpeg", "image/png", "image/webp"];



const signupSchema = z
  .object({
    schoolName: z.string().min(2, "School name must be at least 2 characters"),
    employeeName: z.string().min(2, "Employee name must be at least 2 characters"),
    religion: z.string().optional(),
    nationality: z.string().optional(),
    contact: z
      .string()
      .min(10, "Contact must be at least 10 digits")
      .regex(/^[0-9+\-\s()]+$/, "Invalid contact number"),
    email: z.string().email("Invalid email address"),
    password: z
      .string()
      .min(8, "Password must be at least 8 characters")
      .regex(/[A-Z]/, "Must contain at least one uppercase letter")
      .regex(/[0-9]/, "Must contain at least one number"),
    confirmPassword: z.string(),
    address: z.string().min(5, "Address must be at least 5 characters"),
    photo: z
      .any()
      .refine((files) => files?.length === 1, "Photo is required")
      .refine(
        (files) => files?.[0]?.size <= MAX_FILE_SIZE,
        "Max file size is 5MB"
      )
      .refine(
        (files) => ACCEPTED_IMAGE_TYPES.includes(files?.[0]?.type),
        "Only .jpg, .jpeg, .png and .webp formats are supported"
      ),
    cnicFront: z
      .any()
      .refine((files) => files?.length === 1, "CNIC front is required")
      .refine((files) => files?.[0]?.size <= MAX_FILE_SIZE, "Max file size is 5MB")
      .refine(
        (files) => ACCEPTED_IMAGE_TYPES.includes(files?.[0]?.type),
        "Only .jpg, .jpeg, .png and .webp formats are supported"
      ),
    cnicBack: z
      .any()
      .refine((files) => files?.length === 1, "CNIC back is required")
      .refine((files) => files?.[0]?.size <= MAX_FILE_SIZE, "Max file size is 5MB")
      .refine(
        (files) => ACCEPTED_IMAGE_TYPES.includes(files?.[0]?.type),
        "Only .jpg, .jpeg, .png and .webp formats are supported"
      ),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });


  export default signupSchema;

