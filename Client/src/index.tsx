import React from "react";

import ReactDOM from "react-dom";

import Max from "@components/Max";

const App = () => {
  return (
    <>
      <Max />
      <h1>TestPoint</h1>
    </>
  );
};

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);
