import React, { useState } from "react";
import "@shoelace-style/shoelace/dist/themes/light.css";
import {
  SlDrawer,
  SlButton,
  SlMenu,
  SlMenuItem,
} from "@shoelace-style/shoelace/dist/react";

function HeaderNav() {
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);

  const toggleDrawer = () => {
    setIsDrawerOpen(!isDrawerOpen);
  };

  const logoAndBrand = (
    <>
      <div style={{ display: "flex", alignItems: "center" }}>
        <img
          src="/finebe-icon.png"
          alt="Logo"
          style={{ height: "40px", marginRight: "1rem" }}
        />
        <span style={{ fontSize: "1.5rem", fontWeight: 500 }}>finebe</span>
      </div>
    </>
  );

  return (
    <>
      <nav
        style={{
          padding: "1rem",
          backgroundColor: "#f0f0f0",
          display: "flex",
          alignItems: "center",
          justifyContent: "space-between",
          borderBottom: "1px solid #ccc",
        }}
      >
        {logoAndBrand}
        <SlButton onClick={toggleDrawer} pill>
          <sl-icon name="list"></sl-icon>
        </SlButton>
      </nav>

      <SlDrawer
        label="finebe"
        open={isDrawerOpen}
        onSlAfterHide={() => setIsDrawerOpen(false)}
      >
        <SlMenu>
          <SlMenuItem>Home</SlMenuItem>
          <SlMenuItem>About</SlMenuItem>
          <SlMenuItem>Services</SlMenuItem>
          <SlMenuItem>Contact</SlMenuItem>
        </SlMenu>
      </SlDrawer>
    </>
  );
}

export default HeaderNav;
