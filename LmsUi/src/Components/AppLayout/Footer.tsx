import React from 'react';

const Footer: React.FC = () => {
  return (
    <footer className="w-full bg-transparent py-8 mt-auto">
      <div className="max-w-[95%] xl:max-w-[1400px] mx-auto px-6 border-t border-gray-100 pt-6">
        <div className="flex flex-col md:flex-row justify-between items-center gap-4">
          
          {/* Minimal Brand / Copyright */}
          <div className="text-sm text-gray-400 font-medium">
            © 2026 <span className="text-gray-500">lmsui</span>. All rights reserved.
          </div>

          {/* Blank/Empty Space style links */}
          <div className="flex items-center gap-8 text-xs font-medium uppercase tracking-widest text-gray-400">
            <a href="#" className="hover:text-blue-500 transition-colors">Privacy</a>
            <a href="#" className="hover:text-blue-500 transition-colors">Terms</a>
            <a href="#" className="hover:text-blue-500 transition-colors">Contact</a>
          </div>
          
        </div>
      </div>
    </footer>
  );
};

export default Footer;