/* eslint-disable @typescript-eslint/no-explicit-any */

declare module "*.json" {
  const value: any;
  export default value;
}

declare module "*.png" {
  const value: `${string}.png`;
  export default value;
}

declare module "*.webp" {
  const value: `${string}.webp`;
  export default value;
}

declare module "*.jpeg" {
  const value: `${string}.jpeg`;
  export default value;
}

declare module "*.jpg" {
  const value: `${string}.jpg`;
  export default value;
}

declare module "*.svg" {
  const value: `${string}.svg`;
  export default value;
}

declare module "*.html" {
  const value: `${string}.html`;
  export default value;
}

declare module "*.md" {
  const value: `${string}.md`;
  export default value;
}
