export interface pagination <T>{
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T;
}
