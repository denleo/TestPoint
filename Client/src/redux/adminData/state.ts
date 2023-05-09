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

export interface TestInfo {
  id: string;
  author: string;
  name: string;
  difficulty: TestDifficulty;
  questionCount: 0;
  estimatedTime: 0;
}

export interface TestData {
  id: string;
  name: string;
  difficulty: 0;
  estimatedTime: 0;
  authorId: string;
  questions: [
    {
      id: string;
      questionText: string;
      questionType: 0;
      answers: [
        {
          id: string;
          answerText: string;
          isCorrect: boolean;
        }
      ];
    }
  ];
}
