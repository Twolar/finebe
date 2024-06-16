import React from "react";
import { Routes, Route } from "react-router-dom";
import { Container, CssBaseline } from "@mui/material";
import { ThemeProvider } from '@mui/material/styles';
import { ColorModeContext, useMode } from "./theme";

import Home from "./pages/Home";
import About from "./pages/About";
import Contact from "./pages/Contact";
import HeaderBar from "./components/HeaderBar";

function App() {
  const [theme, colorMode] = useMode();

  const pageList = ["Home", "Contact", "About"];
  const settingPageList = ["Profile", "Account", "Dashboard", "Logout"];

  return (
    <>
      <ColorModeContext.Provider value={colorMode}>
        <ThemeProvider theme={theme}>
          <CssBaseline /> 
          <div className="app">
            <HeaderBar pages={pageList} settings={settingPageList}/>
            <main className="content">
              <Container maxWidth="xl">
                <Routes>
                  <Route path="/" element={<Home />} />
                  <Route path="/home" element={<Home />} />
                  <Route path="/contact" element={<Contact />} />
                  <Route path="/about" element={<About />} />
                </Routes>
              </Container>
            </main>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    </>
  );
}

export default App;
