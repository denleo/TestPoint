import { generateUniqueId } from "@/api/generateId";

export enum TestDifficulty {
  Easy,
  Medium,
  Hard,
}

export enum QuestionType {
  SingleOption,
  MultipleOptions,
  TextSubstitution,
}

export interface QuestionVariant {
  id: string;
  text: string;
  isCorrect: boolean;
}

export interface TestQuestion {
  id: string;
  type: QuestionType;
  title?: string;
  question: string;
  variants: QuestionVariant[];
}

export interface TestData {
  id: string;
  name: string;
  author: string;
  difficulty: TestDifficulty;
  questions?: TestQuestion[];
  completionTime?: string;
}

export interface UserTestData {
  testId: string;
  answers: { questionId: number; answerId: number }[];
}

export const TEST_DATA_1: TestData = {
  id: "testTIOP",
  name: "Тестирование и отладка программного обеспечения",
  author: "Maxim Tester",
  difficulty: TestDifficulty.Hard,
  completionTime: "15 min",
  questions: [
    {
      id: generateUniqueId(),
      question: "Функциональное тестирование проводят в рамках ... ",
      type: QuestionType.SingleOption,
      variants: [
        { id: "1", text: "модульного тестирования", isCorrect: false },
        { id: "2", text: "динамического тестирования", isCorrect: false },
        { id: "3", text: "статического тестирования", isCorrect: false },
        { id: "4", text: "автоматизированного тестирования", isCorrect: true },
      ],
    },
    {
      id: generateUniqueId(),
      question: "Функциональное тестирование проводят в рамках 2 ... ",
      type: QuestionType.SingleOption,
      variants: [
        { id: "1", text: "модульного тестирования", isCorrect: true },
        { id: "2", text: "динамического тестирования", isCorrect: false },
        { id: "3", text: "статического тестирования", isCorrect: false },
        { id: "4", text: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      question: "Функциональное тестирование проводят в рамках 3 ... ",
      type: QuestionType.SingleOption,
      variants: [
        { id: "1", text: "модульного тестирования", isCorrect: false },
        { id: "2", text: "динамического тестирования", isCorrect: true },
        { id: "3", text: "статического тестирования", isCorrect: false },
        { id: "4", text: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      question: "Функциональное тестирование проводят в рамках 4 ... ",
      type: QuestionType.SingleOption,
      variants: [
        { id: "1", text: "модульного тестирования", isCorrect: false },
        { id: "2", text: "динамического тестирования", isCorrect: false },
        { id: "3", text: "статического тестирования", isCorrect: true },
        { id: "4", text: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      question: "Функциональное тестирование проводят в рамках 5 ... ",
      type: QuestionType.SingleOption,
      variants: [
        { id: "1", text: "модульного тестирования", isCorrect: true },
        { id: "2", text: "динамического тестирования", isCorrect: false },
        { id: "3", text: "статического тестирования", isCorrect: false },
        { id: "4", text: "автоматизированного тестирования", isCorrect: false },
      ],
    },
  ],
};

export const getInitQuestionVariants = (type: QuestionType) => {
  switch (type) {
    case QuestionType.TextSubstitution:
      return [{ id: generateUniqueId(), text: "answer", isCorrect: true }];
    default:
      return [
        { id: generateUniqueId(), text: "answer 1", isCorrect: true },
        { id: generateUniqueId(), text: "answer 2", isCorrect: false },
        { id: generateUniqueId(), text: "answer 3", isCorrect: false },
        { id: generateUniqueId(), text: "answer 4", isCorrect: false },
      ];
  }
};
