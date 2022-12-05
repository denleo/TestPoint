export interface TestQuestion {
  title?: string;
  question: string;
  variants: { id: number; text: string }[];
  answerId: number;
}

export interface TestData {
  id: string;
  name: string;
  questions: TestQuestion[];
  completionTime: string;
}

export interface UserTestData {
  testId: string;
  answers: { questionId: number; answerId: number }[];
}

export const TEST_DATA_1: TestData = {
  id: "testTIOP",
  name: "Тестирование и отладка программного обеспечения",
  completionTime: "15 min",
  questions: [
    {
      question: "Функциональное тестирование проводят в рамках ... ",
      variants: [
        { id: 1, text: "модульного тестирования" },
        { id: 2, text: "динамического тестирования" },
        { id: 3, text: "статического тестирования" },
        { id: 4, text: "автоматизированного тестирования" },
      ],
      answerId: 2,
    },
    {
      question: "Функциональное тестирование проводят в рамках 2 ... ",
      variants: [
        { id: 1, text: "модульного тестирования" },
        { id: 2, text: "динамического тестирования" },
        { id: 3, text: "статического тестирования" },
        { id: 4, text: "автоматизированного тестирования" },
      ],
      answerId: 2,
    },
    {
      question: "Функциональное тестирование проводят в рамках 3 ... ",
      variants: [
        { id: 1, text: "модульного тестирования" },
        { id: 2, text: "динамического тестирования" },
        { id: 3, text: "статического тестирования" },
        { id: 4, text: "автоматизированного тестирования" },
      ],
      answerId: 2,
    },
    {
      question: "Функциональное тестирование проводят в рамках 4 ... ",
      variants: [
        { id: 1, text: "модульного тестирования" },
        { id: 2, text: "динамического тестирования" },
        { id: 3, text: "статического тестирования" },
        { id: 4, text: "автоматизированного тестирования" },
      ],
      answerId: 2,
    },
    {
      question: "Функциональное тестирование проводят в рамках 5 ... ",
      variants: [
        { id: 1, text: "модульного тестирования" },
        { id: 2, text: "динамического тестирования" },
        { id: 3, text: "статического тестирования" },
        { id: 4, text: "автоматизированного тестирования" },
      ],
      answerId: 2,
    },
  ],
};
