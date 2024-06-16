import { Box, FormControl, FormLabel, Input, Button } from "@mui/material";
import { useTheme } from "@emotion/react";
import React, { useState } from "react";

export default function RegisterForm() {
  const theme = useTheme();

  // State variables to store form data
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (event) => {
    event.preventDefault();
    // Handle form submission logic here (e.g., sending data to backend)
    const formData = {
      name: name,
      email: email,
      password: password,
    };
    console.log(formData);

    // Reset form fields after submission if needed
    setName("");
    setEmail("");
    setPassword("");
  };

  return (
    <Box
      maxWidth={400}
      mx="auto"
      p={3}
      border="solid 1px #ccc"
      borderRadius={8}
      sx={{
        "& .Mui-focused": {
          color: `${theme.palette.secondary.main} !important`,
        },
        "& .Mui-focused::after": {
          borderBottom: `2px solid ${theme.palette.secondary.main}`,
        },
      }}
    >
      <form onSubmit={handleSubmit}>
        <FormControl fullWidth margin="normal" required>
          <FormLabel>Name</FormLabel>
          <Input
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Write your name here"
          />
        </FormControl>

        <FormControl fullWidth margin="normal" required>
          <FormLabel>Email</FormLabel>
          <Input
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            type="email"
            placeholder="Your email address"
          />
        </FormControl>

        <FormControl fullWidth margin="normal" required>
          <FormLabel>Password</FormLabel>
          <Input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            type="password"
            placeholder="Your password"
          />
        </FormControl>

        <Button
          type="submit"
          variant="contained"
          fullWidth
          sx={{
            mt: 3,
            backgroundColor: theme.palette.primary.main,
            "&:hover": {
              backgroundColor: theme.palette.primary.dark,
            },
          }}
        >
          Register
        </Button>
      </form>
    </Box>
  );
}
