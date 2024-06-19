import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  SlDrawer,
  SlButton,
  SlMenu,
  SlMenuItem,
  SlIcon,
} from "@shoelace-style/shoelace/dist/react";

function HeaderNav() {
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);
  const navigate = useNavigate();

  const toggleDrawer = () => {
    setIsDrawerOpen(!isDrawerOpen);
  };

  const handleNavigation = (path) => {
    navigate(path);
    setIsDrawerOpen(false);
  };

  const logoAndBrand = (
    <div style={{ display: "flex", alignItems: "center" }}>
      <img
        src="/finebe-icon.png"
        alt="Logo"
        onClick={() => handleNavigation("/")}
        style={{ cursor: "pointer", height: "40px", marginRight: "1rem" }}
      />
      <span onClick={() => handleNavigation("/")} style={{ cursor: "pointer", fontSize: "1.5rem", fontWeight: 500 }}>finebe</span>
    </div>
  );

  return (
    <>
      <nav
        style={{
          padding: "1rem",
          display: "flex",
          alignItems: "center",
          justifyContent: "space-between",
          boxShadow: "0 1px 6px var(--sl-color-neutral-100)",
          maxWidth: "1500px",
          margin: "auto",
        }}
      >
        {logoAndBrand}
        <SlButton onClick={toggleDrawer} pill>
          <SlIcon name="list"></SlIcon>
        </SlButton>
      </nav>

      <SlDrawer
        label="finebe"
        open={isDrawerOpen}
        onSlAfterHide={() => setIsDrawerOpen(false)}
        style={{ border: "none", padding: "1rem" }}
      >
        <SlMenu style={{ margin: "0" }}>
          <SlMenuItem onClick={() => handleNavigation("/")}>Home</SlMenuItem>
          <SlMenuItem onClick={() => handleNavigation("/about")}>About</SlMenuItem>
          <SlMenuItem onClick={() => handleNavigation("/contact")}>Contact</SlMenuItem>
        </SlMenu>
      </SlDrawer>
    </>
  );
}

export default HeaderNav;
