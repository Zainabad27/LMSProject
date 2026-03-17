import { useEffect, useState } from "react";
import { toast } from "../Toast";
import api from "../AxiosConfig";
import type { CourseDto } from "../Dtos/GetCourses";

type FetchState = "idle" | "loading" | "success" | "error";

const useCourses = (schoolId: string) => {
  const [courses, setCourses] = useState<CourseDto[]>([]);
  const [fetchState, setFetchState] = useState<FetchState>("idle");

  useEffect(() => {
    if (!schoolId) return;

    const fetchCourses = async () => {
      setFetchState("loading");
      try {
        const { data } = await api.get<CourseDto[]>(
          `/Schools/GetAllCourses/${schoolId}`,
        );
        setCourses(data);
        setFetchState("success");
      } catch (error: any) {
        setFetchState("error");
        toast.error(
          error.response?.data?.message ||
            "An error occurred while fetching courses.",
        );
      }
    };

    fetchCourses();
  }, [schoolId]);

  return { courses, fetchState };
};

export default useCourses;
