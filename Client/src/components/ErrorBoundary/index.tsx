/* eslint-disable react/state-in-constructor */
import React, { Component, ErrorInfo, ReactNode } from "react";

import { ErrorScreen } from "@/containers/ErrorScreen";

interface Props {
  children?: ReactNode;
}

interface State {
  hasError: boolean;
  error?: Error;
}

class ErrorBoundary extends Component<Props, State> {
  public state: State = {
    hasError: false,
  };

  public static getDerivedStateFromError(error: Error): State {
    // Update state so the next render will show the fallback UI.
    return { hasError: true, error };
  }

  public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
    console.error("Uncaught error:", error, errorInfo);
  }

  public render() {
    const { error } = this.state;
    if (this.state.hasError) {
      return (
        <ErrorScreen
          error={error}
          message={error?.message ?? "Something went wrong..."}
        />
      );
    }

    return this.props.children;
  }
}

export default ErrorBoundary;
