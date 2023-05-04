import React, { FC } from "react";

import BackupIcon from "@mui/icons-material/Backup";
import ConstructionIcon from "@mui/icons-material/Construction";
import { styled, Typography, Box, Button } from "@mui/material";
import Dropzone from "react-dropzone";

import { useBreakpoint } from "@api/hooks/useBreakPoint";

import dropZoneImg from "./dropzone.jpg";

const LayoutPage = styled("div")(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  flexDirection: "column",
  justifyContent: "center",
  width: "100%",
  height: "100%",
}));

const SelectionBlock = styled(Box)(({ theme }) => ({
  height: 300,
  width: 300,
  border: `3px solid ${theme.palette.secondary.dark}`,
  padding: theme.spacing(3),
  borderRadius: theme.spacing(4),
  display: "flex",
  justifyContent: "space-between",
  alignItems: "center",
  flexDirection: "column",
  gap: theme.spacing(2),
}));

const DropBlock = styled("div")(({ theme }) => ({
  width: "100%",
  flex: 1,
  border: `2px solid ${theme.palette.common.black}`,
  padding: theme.spacing(3, 1),
  borderRadius: theme.spacing(2),
  cursor: "pointer",
  display: "flex",
  flexDirection: "column",
  justifyContent: "center",
  alignItems: "center",
}));

interface Props {
  onCreate: () => void;
}

export const StartScreen: FC<Props> = ({ onCreate }) => {
  const mdUp = useBreakpoint("md");

  return (
    <LayoutPage>
      <Typography gutterBottom variant={mdUp ? "h1" : "h3"} align="center">
        Create your new test right now!
      </Typography>
      <Typography variant="h4" align="center">
        Choose the option that suits you best:
      </Typography>
      <Box display="flex" justifyContent="center" gap={mdUp ? 10 : 5} mt={2} flexWrap="wrap">
        <SelectionBlock>
          <Typography>Create a test using the built-in constructor</Typography>
          <ConstructionIcon sx={{ width: 75, height: 75 }} />
          <Button variant="contained" color="secondary" size="large" onClick={onCreate}>
            Create
          </Button>
        </SelectionBlock>
        <SelectionBlock>
          <Dropzone onDrop={(acceptedFiles) => console.log(acceptedFiles)}>
            {({ getRootProps, getInputProps }) => (
              <>
                <Typography>Import a test file to create it</Typography>
                <DropBlock
                  {...getRootProps()}
                  sx={{
                    backgroundImage: `url(${dropZoneImg})`,
                    backgroundSize: "contain",
                    backgroundPosition: "top right",
                  }}
                >
                  <input {...getInputProps()} />
                  {/* <Typography variant="caption">Drag and drop a file here, or click to select file</Typography> */}
                  <BackupIcon fontSize="large" sx={{ width: 75, height: 75 }} color="secondary" />
                </DropBlock>
              </>
            )}
          </Dropzone>
        </SelectionBlock>
      </Box>
    </LayoutPage>
  );
};
