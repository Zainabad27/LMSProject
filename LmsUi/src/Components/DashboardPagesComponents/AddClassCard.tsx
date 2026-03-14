const AddClassCard = ({ onClick }: { onClick: () => void }) => (
  <div 
    onClick={onClick}
    className="w-52 h-36 border-2 border-dashed border-gray-200 rounded-xl flex flex-col items-center justify-center cursor-pointer hover:border-blue-400 hover:bg-blue-50 transition-all group bg-white/50"
  >
    <div className="w-10 h-10 rounded-full bg-gray-50 flex items-center justify-center group-hover:bg-blue-100 transition-colors border border-gray-100">
      <span className="text-2xl text-gray-400 group-hover:text-blue-500">+</span>
    </div>
    <p className="mt-3 text-sm font-semibold text-gray-500 group-hover:text-blue-600">Add New Class</p>
  </div>
);

export default AddClassCard;