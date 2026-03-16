import { useEffect, useState } from "react";
import { toast } from "../Toast";
import api from "../AxiosConfig";
import type { CourseDto } from "../Dtos/GetCourses";

type FetchState = "idle" | "loading" | "success" | "error";

const useClasses = (courseId: string) => {
  const [classes, setClasses] = useState<CourseDto[]>([]);
  const [fetchState, setFetchState] = useState<FetchState>("idle");

  useEffect(() => {
    if (!courseId) return;

    const fetchClasses = async () => {
      setFetchState("loading");
      try {
        const { data } = await api.get<CourseDto[]>(
          `/Class/GetAllClasses/${courseId}`,
        );
        setClasses(data);
        setFetchState("success");
      } catch (error: any) {
        setFetchState("error");
        toast.error(
          error.response?.data?.message ||
            "An error occurred while fetching classes.",
        );
      }
    };

    fetchClasses();
  }, [courseId]);

  return { classes, fetchState };
};

export default useClasses;
