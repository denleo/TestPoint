import { generateUniqueId } from "@/api/generateId";
import avatar1 from "@/shared/avatar1.jpg";
import avatar2 from "@/shared/avatar2.jpg";

export interface UserGroup {
  id: string;
  name: string;
  count: number;
}

export interface UserInfo {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  avatar: string;
}

export const USER_TEST_GROUPS: UserGroup[] = [
  { id: generateUniqueId(), name: "10702319", count: 6 },
  { id: generateUniqueId(), name: "10702419", count: 19 },
  { id: generateUniqueId(), name: "10702519", count: 36 },
];

export const TEST_USERS: UserInfo[] = [
  {
    id: generateUniqueId(),
    firstName: "Aleksei",
    lastName: "Polski",
    email: "123124@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Emma",
    lastName: "Jones",
    email: "emma.jones.4789@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Lucas",
    lastName: "Nguyen",
    email: "lucas.nguyen.9876@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "Maria",
    lastName: "Martinez",
    email: "maria.martinez.2345@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Benjamin",
    lastName: "Chen",
    email: "benjamin.chen.8765@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "Isabella",
    lastName: "Garcia",
    email: "isabella.garcia.3456@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "David",
    lastName: "Kim",
    email: "david.kim.5678@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Sophia",
    lastName: "Ng",
    email: "sophia.ng.4321@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "Ethan",
    lastName: "Wong",
    email: "ethan.wong.7654@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Olivia",
    lastName: "Lee",
    email: "olivia.lee.3210@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "William",
    lastName: "Zhang",
    email: "william.zhang.6543@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Ava",
    lastName: "Wu",
    email: "ava.wu.2109@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "James",
    lastName: "Tran",
    email: "james.tran.9870@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Emily",
    lastName: "Chan",
    email: "emily.chan.7890@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Michael",
    lastName: "Hu",
    email: "michael.hu.0987@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "Mia",
    lastName: "Li",
    email: "mia.li.5678@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Alexander",
    lastName: "Wang",
    email: "alexander.wang.2345@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "Sofia",
    lastName: "Liu",
    email: "sofia.liu.8765@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "Daniel",
    lastName: "Chang",
    email: "daniel.chang.3456@gmail.com",
    avatar: avatar2,
  },
  {
    id: generateUniqueId(),
    firstName: "Chloe",
    lastName: "Chua",
    email: "chloe.chua.5678@gmail.com",
    avatar: avatar1,
  },
  {
    id: generateUniqueId(),
    firstName: "Matthew",
    lastName: "Tan",
    email: "matthew.tan.4321@gmail.com",
    avatar: avatar1,
  },
];
