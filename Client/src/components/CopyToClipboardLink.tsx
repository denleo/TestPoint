import React, { useState, FC, useCallback, MouseEvent } from "react";

import { Link, LinkProps, Snackbar } from "@mui/material";

export const CopyToClipboardLink: FC<LinkProps> = ({ href, children, ...props }) => {
  const [open, setOpen] = useState(false);

  const handleClick = useCallback(
    (e: MouseEvent<HTMLAnchorElement>) => {
      if (!href) return;
      setOpen(true);
      e.stopPropagation();
      e.preventDefault();
      navigator.clipboard.writeText(href);
    },
    [href]
  );

  return (
    <>
      <Link href={href} onClick={handleClick} {...props}>
        {children}
      </Link>
      <Snackbar open={open} onClose={() => setOpen(false)} autoHideDuration={2000} message="Copied to clipboard" />
    </>
  );
};
