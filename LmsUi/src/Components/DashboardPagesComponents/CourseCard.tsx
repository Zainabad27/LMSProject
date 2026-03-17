import { Trash2 } from "lucide-react"; 
import type { CourseDto } from "../../Dtos/GetCourses";

const CourseCard = ({ CourseItem, onDelete }: { CourseItem: CourseDto, onDelete: (id: string) => void }) => (
  <div className="group relative bg-white border border-gray-200 rounded-xl p-5 w-52 h-36 shadow-sm hover:shadow-md transition-all duration-200 flex flex-col justify-between">
    
    <button 
      onClick={() => onDelete(CourseItem.courseId)}
      className="absolute top-3 right-3 p-1.5 text-gray-400 hover:text-red-500 hover:bg-red-50 rounded-lg opacity-0 group-hover:opacity-100 transition-all"
    >
      <Trash2 size={16} />
    </button>

    <div>
      <h3 className="text-[15px] font-bold text-gray-800 truncate pr-6">
        {CourseItem.courseName}
      </h3>
      <p className="text-sm text-gray-500 mt-1">
        <span className="font-medium text-gray-600">Board:</span>{" "}
        {CourseItem.board}
      </p>
    </div>

    <div className="mt-2">
       <span className="text-[10px] font-mono text-gray-400 bg-gray-50 border border-gray-100 rounded px-1.5 py-0.5 block truncate">
        {CourseItem.courseId}
      </span>
    </div>
  </div>
);

export default CourseCard;