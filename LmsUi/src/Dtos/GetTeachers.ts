interface TeacherDto {
  teacherId: string;
  teacherName: string;
}

export interface PaginatedTeachers {
  items: TeacherDto[];
  pageNumber: number;
  pageSize: number;
  totalItems: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
