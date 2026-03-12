import React from 'react';
import { useNavigate } from 'react-router-dom';

interface DialogBoxProps {
  title: string;      // e.g., "Assignment Box"
  destination: string; // e.g., "/admin/assignments"
  description?: string; // Optional subtitle
  icon?: React.ReactNode;
}

const DialogBox: React.FC<DialogBoxProps> = ({ title, destination, description, icon }) => {
  const navigate = useNavigate();

  return (
    <div
      onClick={() => navigate(destination)}
      className="group cursor-pointer overflow-hidden rounded-xl border border-blue-200 bg-white p-6 
                 shadow-sm transition-all duration-300 hover:border-blue-500 hover:bg-blue-50 hover:shadow-md 
                 active:scale-95 flex flex-col items-start gap-3"
    >
      {/* Icon Wrapper (Optional) */}
      {icon && (
        <div className="rounded-lg bg-blue-100 p-3 text-blue-600 group-hover:bg-blue-600 group-hover:text-white transition-colors">
          {icon}
        </div>
      )}

      <div>
        <h3 className="text-lg font-semibold text-slate-800 group-hover:text-blue-700 transition-colors">
          {title}
        </h3>
        {description && (
          <p className="mt-1 text-sm text-slate-500">
            {description}
          </p>
        )}
      </div>

      {/* Little "Go" indicator */}
      <div className="mt-2 text-xs font-medium text-blue-500 opacity-0 group-hover:opacity-100 transition-opacity">
        Click to manage →
      </div>
    </div>
  );
};

export default DialogBox;