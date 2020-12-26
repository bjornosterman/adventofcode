#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <time.h>

/*
  This bruteforce solved it in about 3 hours
  The day after I figured out how to solve it fast
*/

void main()
{
	printf("Start!\n");

	int use_sample = 0;
	int part = 2;

	int len = part == 1 ? 9 : use_sample ? 1000000 : 1000000;
	int iterations = part == 1 ? 100 : 10000000;
	int sample[] = {3, 8, 9, 1, 2, 5, 4, 6, 7};
	int input[] = {5, 8, 3, 9, 7, 6, 2, 4, 1};

	int *array1 = (int *)malloc(sizeof(int) * len);

	// Initialize array
	for (int i = 0; i < len; i++)
	{
		array1[i] = i < 9 ? ((use_sample ? sample : input)[i]) : i + 1;
	}

	clock_t main_clock = clock();
	clock_t iter_clock = clock();

	setbuf(stdout, NULL);
	for (int i = 0; i < iterations; i++)
	{
		if (i % 100000 == 0)
		{
			printf("%i: %f sec\n", i, ((double)clock() - iter_clock) / CLOCKS_PER_SEC);
			iter_clock = clock();
		}
		// for (int p = 0; p < 9; p++)
		// {
		// 	printf("%d ", array1[p]);
		// }
		// printf("\n");

		int cc_value, a, b, c;
		cc_value = array1[0];
		a = array1[1];
		b = array1[2];
		c = array1[3];

		// printf("pick up: %i, %i, %i\n", a, b, c);

		int dc;
		int dc_value = cc_value - 1;
		while (dc_value == a || dc_value == b || dc_value == c || dc_value < 1)
		{
			dc_value--;
			if (dc_value < 1)
				dc_value = len;
		}

		// printf("destination: %i\n", dc_value);

		int g;
		for (g = dc; array1[g] != dc_value; g--) {
			if (g <= 0) g = len;
		}
		dc = g;
		// for (dc = len - 1; array1[dc] != dc_value; dc--)
		// 	;
		// printf("destination index: %i\n", dc);
		memmove(array1, array1 + 4, (dc - 3) * sizeof(int));
		memmove(array1 + dc, array1 + dc + 1, (len - dc) * sizeof(int));
		array1[dc - 3] = a;
		array1[dc - 2] = b;
		array1[dc - 1] = c;
		array1[len - 1] = cc_value;
	}

	int one;
	for (one = 0; array1[one] != 1; one++)
		;

	if (part == 1)
	{
		printf("Answer: ");
		for (int i = 1; i < len; i++)
		{
			printf("%i", array1[(i + one) % len]);
		}
		printf("\n");
	}
	else
	{
		printf("Answer: %i * %i = %i", array1[one + 1], array1[one + 2], array1[one + 1] * array1[one + 2]);
	}
	printf("\n");

	double time_taken = ((double)clock() - main_clock) / CLOCKS_PER_SEC;
	printf("The program took %f seconds to execute %i terations\n", time_taken, iterations);

	printf("Done!\n");
}
