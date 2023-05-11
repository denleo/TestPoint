export interface UserAnswer {
  questionId: string;
  answers: [string];
}

export interface TestResult {
  score: number;
  correctAnswersCount: number;
  completionTime: number;
  history: UserAnswer[];
}
