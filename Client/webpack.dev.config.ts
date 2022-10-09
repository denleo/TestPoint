import path from "path";

import CopyWebpackPlugin from "copy-webpack-plugin";
import HtmlWebpackPlugin from "html-webpack-plugin";
import TsconfigPathsPlugin from "tsconfig-paths-webpack-plugin";
import {
  Configuration as WebpackConfiguration,
  HotModuleReplacementPlugin,
} from "webpack";
import { Configuration as WebpackDevServerConfiguration } from "webpack-dev-server";

interface Configuration extends WebpackConfiguration {
  devServer?: WebpackDevServerConfiguration;
}

const src = path.join(__dirname, "src");
const dist = path.join(__dirname, "dist");

const config: Configuration = {
  mode: "development",
  output: {
    publicPath: "/",
  },
  entry: "./src/index.tsx",
  module: {
    rules: [
      {
        test: /\.(ts|js)x?$/i,
        exclude: /node_modules/,
        use: {
          loader: "babel-loader",
          options: {
            presets: [
              "@babel/preset-env",
              "@babel/preset-react",
              "@babel/preset-typescript",
            ],
          },
        },
      },
    ],
  },
  resolve: {
    extensions: [".tsx", ".ts", ".js"],
    alias: {
      "@": path.resolve(__dirname, "src"),
      "@components": path.resolve(__dirname, "src/components"),
      "@containers": path.resolve(__dirname, "src/containers"),
      "@layout": path.resolve(__dirname, "src/layout"),
      "@constants": path.resolve(__dirname, "src/constants"),
      "@common": path.resolve(__dirname, "src/common"),
      "@api": path.resolve(__dirname, "src/api"),
    },
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: path.join(src, "index.html"),
    }),
    new HotModuleReplacementPlugin(),
    // new TsconfigPathsPlugin(),
    new CopyWebpackPlugin({
      patterns: ["common/favicon", "common/styles"].map((e: any) => ({
        from: path.join(src, e.from || e),
        to: path.join(dist, e.to || e),
      })),
    }),
  ],
  devtool: "inline-source-map",
  devServer: {
    static: path.join(__dirname, "dist"),
    historyApiFallback: true,
    port: 4000,
    open: true,
    hot: true,
  },
};

export default config;
