import Footer from "./Footer";
import Header from "./Header";
import { Outlet } from "react-router-dom";

const AppLayout = () => {
  return (
    // min-h-screen + flex-col ensures the footer stays at the bottom of the page
    <div className="min-h-screen flex flex-col">
      <Header />
      
      {/* pt-20 (Padding Top) pushes the Outlet content down from behind the fixed Header. 
          flex-grow ensures this area fills the screen so the footer isn't floating in the middle.
      */}
      <main className="flex-grow pt-20">
        <Outlet />
      </main>

      <Footer />
    </div>
  );
};

export default AppLayout;