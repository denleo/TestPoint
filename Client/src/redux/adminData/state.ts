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
  answerText: string;
  isCorrect: boolean;
}

export interface TestInfo {
  id: string;
  author: string;
  name: string;
  difficulty: TestDifficulty;
  questionCount: number;
  estimatedTime: number;
}

export interface TestQuestion {
  id: string;
  questionText: string;
  questionType: QuestionType;
  answers: QuestionVariant[];
}

export interface TestData {
  id: string;
  name: string;
  difficulty: TestDifficulty;
  estimatedTime: number;
  authorId: string;
  questions: TestQuestion[];
}
