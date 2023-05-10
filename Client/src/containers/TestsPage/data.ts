import { generateUniqueId } from "@/api/generateId";
import { QuestionType, TestData, TestDifficulty } from "@/redux/adminData/state";

export interface UserTestData {
  testId: string;
  answers: { questionId: number; answerId: number }[];
}

export const TEST_DATA_1: TestData = {
  id: "testTIOP",
  name: "Тестирование и отладка программного обеспечения",
  authorId: "Maxim Tester",
  difficulty: TestDifficulty.Hard,
  estimatedTime: 15,
  questions: [
    {
      id: generateUniqueId(),
      questionText:
        "Функциональное тестирование проводят в рамках Функциональное тестирование проводят в рамкахФункциональное тестирование проводят в рамкахФункциональное тестирование проводят в рамкахФункциональное тестирование проводят в рамкахФункциональное тестирование проводят в рамкахФункциональное тестирование проводят в рамках",
      questionType: QuestionType.TextSubstitution,
      answers: [{ id: "1", answerText: "модульного тестирования", isCorrect: false }],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 2 ... ",
      questionType: QuestionType.SingleOption,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
        { id: "5", answerText: "модульного тестирования", isCorrect: true },
        { id: "6", answerText: "динамического тестирования", isCorrect: false },
        { id: "7", answerText: "статического тестирования", isCorrect: false },
        { id: "8", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 3 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: false },
        { id: "2", answerText: "динамического тестирования", isCorrect: true },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 4 ... ",
      questionType: QuestionType.SingleOption,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: false },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: true },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
    {
      id: generateUniqueId(),
      questionText: "Функциональное тестирование проводят в рамках 5 ... ",
      questionType: QuestionType.MultipleOptions,
      answers: [
        { id: "1", answerText: "модульного тестирования", isCorrect: true },
        { id: "2", answerText: "динамического тестирования", isCorrect: false },
        { id: "3", answerText: "статического тестирования", isCorrect: false },
        { id: "4", answerText: "автоматизированного тестирования", isCorrect: false },
      ],
    },
  ],
};

export const getInitQuestionVariants = (type: QuestionType) => {
  switch (type) {
    case QuestionType.TextSubstitution:
      return [{ id: generateUniqueId(), answerText: "answer", isCorrect: true }];
    default:
      return [
        { id: generateUniqueId(), answerText: "answer 1", isCorrect: true },
        { id: generateUniqueId(), answerText: "answer 2", isCorrect: false },
        { id: generateUniqueId(), answerText: "answer 3", isCorrect: false },
        { id: generateUniqueId(), answerText: "answer 4", isCorrect: false },
      ];
  }
};
