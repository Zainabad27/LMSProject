import type { ClassDto } from "../../Dtos/GetClasses";

const ClassCard = ({ classItem }: { classItem: ClassDto }) => (
  <div className="bg-white border border-gray-200 rounded-xl p-4 w-48 shadow-sm hover:shadow-md transition-shadow duration-200">
    <h3 className="text-sm font-semibold text-gray-800 mb-2 truncate">
      {classItem.className}
    </h3>
    <p className="text-xs text-gray-500 mb-3">
      <span className="font-medium text-gray-600">Strength:</span>{" "}
      {classItem.strength}
    </p>
    <span className="text-[10px] text-gray-400 bg-gray-100 rounded px-1.5 py-0.5">
      {classItem.classId}
    </span>
  </div>
);

export default ClassCard;