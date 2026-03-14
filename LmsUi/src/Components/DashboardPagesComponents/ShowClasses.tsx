import ClassCard from "./ClassesCard";
import useClasses from "../../Hooks/UseClasses";
import { useState } from "react";
import api from "../../AxiosConfig";
import AddClassCard from "./AddClassCard";

interface ShowClassesProps {
  SchoolId: string;
}
interface EmptyStateProps {
  msg: string;
  description?:string
}

const LoadingState = () => (
  <div className="flex justify-center items-center w-full py-16">
    <div className="flex flex-col items-center gap-3">
      <div className="w-6 h-6 border-2 border-gray-300 border-t-blue-500 rounded-full animate-spin" />
      <p className="text-sm text-gray-400">Loading classes...</p>
    </div>
  </div>
);

const EmptyState = (props: EmptyStateProps) => (
  <div className="flex flex-col justify-center items-center w-full py-16 gap-2">
    <p className="text-sm font-medium text-gray-500">{props.msg}</p>
    <p className="text-xs text-gray-400">
      {props.description}
    </p>
  </div>
);


const ShowClasses = ({ SchoolId }: ShowClassesProps) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { classes, fetchState } = useClasses(SchoolId);

  const onDelete = async (id: string) => {
    if(window.confirm("Are you sure you want to delete this class?")) {
        await api.delete(`classes/${id}`); // first have to write this api in the backend then i'll apply it here.
    }
  };

  if (fetchState === "loading") return <LoadingState />;
  if (fetchState === "error") return <EmptyState msg="Error occurred while fetching the Classes." />;

  return (
    <div className="flex flex-wrap gap-6 p-6">
      {/* Always show Add Card first */}
      <AddClassCard onClick={() => setIsModalOpen(true)} />

      {classes.map((classItem) => (
        <ClassCard key={classItem.classId} classItem={classItem} onDelete={onDelete}/>
      ))}

      {/* Logic for your Form Modal can go here or in the parent page */}
      {isModalOpen && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-sm">
           <div className="bg-white p-8 rounded-2xl shadow-xl w-96">
              <h2 className="text-xl font-bold mb-4">Add New Class</h2>
              {/* Form Fields will go here */}
              <button onClick={() => setIsModalOpen(false)} className="text-blue-600">Close</button>
           </div>
        </div>
      )}
    </div>
  );
};

export default ShowClasses;
