module.exports = {
  env: {
    browser: true,
    es2021: true,
  },
  extends: [
    "plugin:react/recommended",
    "airbnb",
    "plugin:prettier/recommended",
    "plugin:@typescript-eslint/recommended",
    "plugin:import/recommended",
    "plugin:import/typescript",
  ],
  parser: "@typescript-eslint/parser",
  parserOptions: {
    ecmaFeatures: {
      jsx: true,
    },
    ecmaVersion: "latest",
    sourceType: "module",
  },
  plugins: ["react", "@typescript-eslint", "import"],
  rules: {
    "react/jsx-filename-extension": ["error", { extensions: [".tsx"] }],
    "react/require-default-props": "off",
    "react/jsx-props-no-spreading": "off",
    "react/destructuring-assignment": 0,
    "react/function-component-definition": [2, { namedComponents: "arrow-function" }],
    "no-shadow": "off",
    "@typescript-eslint/no-shadow": ["error"],
    "prettier/prettier": [
      "error",
      {
        endOfLine: "auto",
        printWidth: 120,
        overrides: [
          {
            files: ["*.tsx", "*.ts"],
            options: {
              semi: false,
            },
          },
        ],
      },
    ],

    // Organize import section.
    "import/no-unresolved": "error",
    "import/prefer-default-export": "off",
    "import/extensions": [
      "error",
      "ignorePackages",
      {
        js: "never",
        jsx: "never",
        ts: "never",
        tsx: "never",
      },
    ],
    "import/no-extraneous-dependencies": [
      "error",
      {
        devDependencies: true,
        optionalDependencies: false,
        peerDependencies: false,
      },
    ],
    "import/order": [
      "error",
      {
        "newlines-between": "always",
        groups: ["builtin", "external", "internal", "parent", "unknown", "sibling"],
        pathGroups: [
          { pattern: "react", group: "external", position: "before" },
          { pattern: "@/**", group: "internal", position: "before" },
        ],
        pathGroupsExcludedImportTypes: ["builtin"],
        alphabetize: { order: "asc", caseInsensitive: true },
      },
    ],
    // "import/newline-after-import": ["error", { count: 2 }], conflict with prettier
  },
  settings: {
    "import/parsers": {
      "@typescript-eslint/parser": [".ts", ".tsx"],
    },
    "import/resolver": {
      typescript: {
        alwaysTryTypes: true,
        project: [`${__dirname}/tsconfig.json`],
      },
    },
  },
};
