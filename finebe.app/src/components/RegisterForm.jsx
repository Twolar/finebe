import React, { useState } from "react";
import {
  Box,
  FormControl,
  FormLabel,
  Input,
  Button
} from "@mui/material";
import { useTheme } from "@emotion/react";

export default function RegisterForm() {
  const theme = useTheme();

  // State variable to store form data
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = (event) => {
    event.preventDefault();

    // Validate if passwords match
    if (formData.password !== formData.confirmPassword) {
      alert("Passwords do not match");
      return;
    }

    // Handle form submission logic here (e.g., sending data to backend)
    console.log(formData);

    // Reset form fields after submission if needed
    setFormData({
      name: "",
      email: "",
      password: "",
      confirmPassword: "",
    });
  };

  return (
    <Box
      maxWidth={400}
      mx="auto"
      p={3}
      border="solid 1px #ccc"
      borderRadius={2}
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
            name="name"
            value={formData.name}
            onChange={handleChange}
            placeholder="Write your name here"
          />
        </FormControl>

        <FormControl fullWidth margin="normal" required>
          <FormLabel>Email</FormLabel>
          <Input
            name="email"
            value={formData.email}
            onChange={handleChange}
            type="email"
            placeholder="Your email address"
          />
        </FormControl>

        <FormControl fullWidth margin="normal" required>
          <FormLabel>Password</FormLabel>
          <Input
            name="password"
            value={formData.password}
            onChange={handleChange}
            type="password"
            placeholder="Your password"
          />
        </FormControl>

        <FormControl fullWidth margin="normal" required>
          <FormLabel>Confirm Password</FormLabel>
          <Input
            name="confirmPassword"
            value={formData.confirmPassword}
            onChange={handleChange}
            type="password"
            placeholder="Confirm your password"
          />
        </FormControl>

        <Button
          type="submit"
          variant="contained"
          fullWidth
          sx={{
            mt: 3,
            backgroundColor: theme.palette.neutral.main,
            "&:hover": {
              backgroundColor: theme.palette.secondary.main,
            },
          }}
        >
          Register
        </Button>
      </form>
    </Box>
  );
}
